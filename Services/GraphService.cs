using System;
using BlazorSample.Data;
using Microsoft.Graph;

namespace BlazorSample.Services
{
	public class GraphService
	{
        private GraphServiceClient _graphServiceClient;

        public GraphService(GraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient;
        }

        public async Task<IUserMessagesCollectionPage> GetEmailByFrom(String filter)
        {
            return await _graphServiceClient.Me.Messages
                .Request()
                .Filter($"(from/emailAddress/address) eq '{filter}'")
                .GetAsync();
        }

        public async Task<IUserMessagesCollectionPage> GetEmailBySubject(String filter)
        {
            return await _graphServiceClient.Me.Messages
                .Request()
                .Filter($"(subject) eq '{filter}'")
                .GetAsync();
        }

        public async Task<IMessageAttachmentsCollectionPage> GetAttachments(String idEmail)
        {
            return await _graphServiceClient.Me.Messages[idEmail].Attachments
                .Request()
                .GetAsync();
        }

        public async Task<DriveItem> GetDrive()
        {
            return await _graphServiceClient.Me.Drive.Root
                .Request()
                .GetAsync();
        }

        public async Task<ITodoListsCollectionPage> GetTodo(String name)
        {
            return await _graphServiceClient.Me.Todo.Lists
                .Request()
                .Filter($"(displayName) eq '{name}'")
                .GetAsync();
        }

        public async Task CreateDrive(FileAttachment fileAttachment)
        {
            using var stream = new System.IO.MemoryStream(fileAttachment.ContentBytes);

            var retorno = await _graphServiceClient.Me.Drive.Root
                .ItemWithPath(fileAttachment.Name).Content
                .Request()
                .PutAsync<DriveItem>(stream);
        }

        public async Task CreateTodo(String id, String title, String body)
        {
            var request = new TodoTask
            {
                Title = title,
                Body = new ItemBody
                {
                    Content = body
                }
            };
            await _graphServiceClient.Me.Todo.Lists[id].Tasks
                .Request()
                .AddAsync(request);
        }

        public async Task CreateCalendar(String subject, String body, DateTime StartDate, DateTime EndDate, String Attendee)
        {
            var request = new Event
            {
                Subject = subject,
                Body = new ItemBody
                {
                    ContentType = BodyType.Html,
                    Content = body,
                },
                Start = new DateTimeTimeZone
                {
                    DateTime = StartDate.ToString("s"),
                    TimeZone = TimeZoneInfo.Local.Id
                },
                End = new DateTimeTimeZone
                {
                    DateTime = EndDate.ToString("s"),
                    TimeZone = TimeZoneInfo.Local.Id
                },
                Attendees = new List<Attendee>
                {
                    new Attendee
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = Attendee
                        },
                        Type = AttendeeType.Required,
                    },
                },
            };
            await _graphServiceClient.Me.Calendar.Events
                .Request()
                .AddAsync(request);
        }
    }
}
