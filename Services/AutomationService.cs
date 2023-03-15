using System;
using BlazorSample.Data;
using Microsoft.Graph;

namespace BlazorSample.Services
{
	public class AutomationService
	{
        private GraphService _graphService;

        private static List<Automation> automations = new List<Automation>();

        public AutomationService(GraphService graphService)
        {
            _graphService = graphService;
        }

        public void Create(Automation automation)
        {
            ValidateCalendar(automation);
            automations.Add(automation);
        }

        public List<Automation> GetAll()
        {
            return automations;
        }

        public async Task Run(String Id)
        {
            var automation = automations.FirstOrDefault(x => x.Id == Id);

            if (automation != null)
            {
                var emails = !string.IsNullOrEmpty(automation.Email.From) ? await _graphService.GetEmailByFrom(automation.Email.From) : await _graphService.GetEmailBySubject(automation.Email.Subject);

                if (!string.IsNullOrEmpty(automation.Email.Attachment))
                {
                    foreach (var email in emails)
                    {
                        var attachment = await GetAttachment(email.Id, automation.Email.Attachment);
                        if (attachment != null)
                            await CreateDriveItem(attachment);
                    }
                }

                if (!string.IsNullOrEmpty(automation.Calendar.Subject))
                {
                    await CreateCalendarEvent(automation.Calendar.Subject, automation.Calendar.Body, automation.Calendar.StartWith, automation.Calendar.EndWith, automation.Calendar.Attendee);
                }

                if (!string.IsNullOrEmpty(automation.Todo.Title))
                {
                    await CreateTodoTask(automation.Todo.Name, automation.Todo.Title, automation.Todo.Body);
                }
            }
        }

        private async Task<FileAttachment?> GetAttachment(String email_id, String attachment_name)
        {
            var attachments = await _graphService.GetAttachments(email_id);
            var result = attachments.FirstOrDefault(x => x.Name.Equals(attachment_name));
            return result != null ? (FileAttachment)result : null;
        }

        private async Task CreateDriveItem(FileAttachment fileAttachment)
        {
            var result = await _graphService.GetDrive();
            await _graphService.CreateDrive(fileAttachment);
        }

        private async Task CreateTodoTask(String Name, String Title, String Body)
        {
            var result = await _graphService.GetTodo(Name);
            await _graphService.CreateTodo(result?.FirstOrDefault()?.Id, Title, Body);
        }

        private async Task CreateCalendarEvent(String Subject, String Body, DateTime StartWith, DateTime EndWith, String Attendee)
        {
            await _graphService.CreateCalendar(Subject, Body, StartWith, EndWith, Attendee);
        }

        private void ValidateCalendar(Automation automation)
        {
            if (automation.Calendar.Active)
            {
                var year = automation.Calendar.Date.Year;
                var month = automation.Calendar.Date.Month;
                var day = automation.Calendar.Date.Day;
                automation.Calendar.StartWith = new DateTime(year, month, day, automation.Calendar.StartWith.Hour, automation.Calendar.StartWith.Minute, automation.Calendar.StartWith.Second);
                automation.Calendar.EndWith = new DateTime(year, month, day, automation.Calendar.EndWith.Hour, automation.Calendar.EndWith.Minute, automation.Calendar.EndWith.Second);
            }
        }
    }
}

