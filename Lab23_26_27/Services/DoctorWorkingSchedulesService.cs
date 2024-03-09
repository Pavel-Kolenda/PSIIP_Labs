using AutoMapper;
using Contracts;
using Entities.Exceptions.DoctorWorkingSchedule;
using HospitalApi.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Shared.Dtos.DoctorWorkingSchedules;
using Shared.Dtos.WorkingSchedules;
using System.Reflection.Metadata;
using Xceed.Words.NET;

namespace Services;
public class DoctorWorkingSchedulesService : IDoctorWorkingSchedulesService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    const int maxWorkingDays = 5;

    public DoctorWorkingSchedulesService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DoctorWorkingScheduleDto>> GetAllAsync(int hospitalId, bool trackChanges)
    {
        var doctors = _repositoryManager
            .DoctorsRepository
            .GetAllAsync(trackChanges)
            .Where(d => d.HospitalId == hospitalId)
            .ToList();

        List<DoctorWorkingScheduleDto> dwsDto = new();

        foreach (Doctor doctor in doctors)
        {
            dwsDto.Add(await GetAsync(hospitalId, doctor.Id, trackChanges));
        }

        return dwsDto;
    }

    public async Task<DoctorWorkingScheduleDto> GetAsync(int hospitalId, int doctorId, bool trackChanges)
    {
        var doctorWorkingSchedules = await _repositoryManager
               .DoctorWorkingSchedulesRepository
               .GetForDoctorAsync(hospitalId, doctorId, trackChanges);

        var groupedSchedules = doctorWorkingSchedules
            .GroupBy(ws => ws.WorkingSchedule.DayOfWeek)
            .Select(group => new WorkingScheduleDto
            {
                Id = group.FirstOrDefault()!.WorkingSchedule.Id,
                DayOfWeek = group.Key,
                StartTime = group.FirstOrDefault()!.WorkingSchedule.StartTime,
                EndTime = group.FirstOrDefault()!.WorkingSchedule.EndTime
            })
            .ToList();

        var doctor = await _repositoryManager.DoctorsRepository.GetAsync(doctorId, trackChanges);

        var doctorWorkingScheduleDto = new DoctorWorkingScheduleDto
        {
            DoctorId = doctor.Id,
            Name = doctor.Name,
            Surname = doctor.Surname,
            Patronymic = doctor.Patronymic,
            DoctorWorkingSchedule = groupedSchedules
        };

        return doctorWorkingScheduleDto;
    }


    public async Task<byte[]> GenerateWordAsync(int hospitalId)
    {
        var schedules = await GetAllAsync(hospitalId, false);

        using (var doc = DocX.Create("DoctorSchedules.docx"))
        {
            foreach (var schedule in schedules)
            {
                doc.InsertParagraph().AppendLine($"Врач: {schedule.Name} {schedule.Surname} {schedule.Patronymic}").Bold();

                var table = doc.AddTable(schedule.DoctorWorkingSchedule.Count() + 1, 3);

                table.Rows[0].Cells[0].Paragraphs.First().Append("День недели");
                table.Rows[0].Cells[1].Paragraphs.First().Append("Начало");
                table.Rows[0].Cells[2].Paragraphs.First().Append("Конец");

                int row = 1;
                foreach (var workingSchedule in schedule.DoctorWorkingSchedule)
                {
                    table.Rows[row].Cells[0].Paragraphs.First().Append(workingSchedule.DayOfWeek.ToString());
                    table.Rows[row].Cells[1].Paragraphs.First().Append(workingSchedule.StartTime.ToString());
                    table.Rows[row].Cells[2].Paragraphs.First().Append(workingSchedule.EndTime.ToString());
                    row++;
                }

                doc.InsertTable(table);
            }

            using (var memoryStream = new MemoryStream())
            {
                doc.SaveAs(memoryStream);
                var content = memoryStream.ToArray();

                return content;
            }
        }
    }

    public async Task CreateAsync(int hospitalId, int doctorId, DoctorWorkingScheduleForCreation doctorWorkingScheduleForCreation)
    {
        var dws = _mapper.Map<DoctorWorkingSchedule>(doctorWorkingScheduleForCreation);

        var doctorWorkingDays = await _repositoryManager
            .DoctorWorkingSchedulesRepository.GetForDoctorAsync(hospitalId, doctorId, false);

        if (doctorWorkingDays.Count() > maxWorkingDays)
        {
            throw new DoctorWorkDaysExceededException
                ($"Doctor with ID {doctorId} has exceeded the maximum limit of {maxWorkingDays} working days.");
        }

        await _repositoryManager.DoctorWorkingSchedulesRepository.CreateAsync(doctorId, dws);

        await _repositoryManager.SaveChangesAsync();
    }
    public async Task UpdateAsync(int doctorId, int dwsId, DoctorWorkingScheduleForUpdate doctorWorkingScheduleForUpdate, bool trackChanges)
    {
        var dws = await _repositoryManager
            .DoctorWorkingSchedulesRepository
            .GetByIdAsync(doctorId, dwsId, trackChanges);

        _mapper.Map(doctorWorkingScheduleForUpdate, dws);

        await _repositoryManager.SaveChangesAsync();
    }

    public async Task DeleteAsync(int doctorId, int dwsId, bool trackChanges)
    {
        var dws = await _repositoryManager
            .DoctorWorkingSchedulesRepository
            .GetByIdAsync(doctorId, dwsId, trackChanges);

        _repositoryManager.DoctorWorkingSchedulesRepository.Remove(dws);

        await _repositoryManager.SaveChangesAsync();
    }
}
