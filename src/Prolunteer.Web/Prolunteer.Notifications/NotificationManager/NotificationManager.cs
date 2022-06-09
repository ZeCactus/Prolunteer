using Prolunteer.DataAccess;
using Prolunteer.Entities;
using Prolunteer.Entities.Enums;
using Prolunteer.Notifications.Email;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prolunteer.Notifications.NotificationManager
{
    public class NotificationManager
    {
        private readonly UnitOfWork uow;
        private readonly MailService MailService;

        public NotificationManager(UnitOfWork unitOfWork, MailService mailService)
        {
            this.uow = unitOfWork;
            this.MailService = mailService;
        }
        public void SendRegistrationNotification(User user)
        {
            var template = uow.NotificationTemplates
                .Get()
                .FirstOrDefault(nt => nt.Id == (int)NotificationTypes.Registration);

            var emailBody = template.Template;

            emailBody.Replace("{FirstName}", user.FirstName);
            emailBody.Replace("{LastName}", user.LastName);

            var email = new Email.Email();

            email.Body = emailBody;
            email.Recipients.Add(user.EMail);
            email.Subject = template.Subject;

            MailService.SendMessage(email);
        }

        public void SendEventCanceledNotification(Event canceledEvent)
        {
            var template = uow.NotificationTemplates
                .Get()
                .FirstOrDefault(nt => nt.Id == (int)NotificationTypes.EventCanceled);

            var users = canceledEvent
                .VolunteerPositions
                .SelectMany(vp => vp.VolunteerParticipations, (vp, volunteers) => volunteers.User)
                .ToList();

            var emails = new List<Email.Email>();

            foreach(var user in users)
            {
                var emailBody = template.Template;

                emailBody.Replace("{EventName}", canceledEvent.Name);
                emailBody.Replace("{EventType}", canceledEvent.EventType.Name);
                emailBody.Replace("{EventLocation}", canceledEvent.Location.Name);

                emailBody.Replace("{EventStartDate}", canceledEvent.StartDate.Date.ToString());
                emailBody.Replace("{EventEndDate}", canceledEvent.EndDate.Date.ToString());

                emailBody.Replace("{EventOrganizerFirstName}", canceledEvent.Organizer.FirstName);
                emailBody.Replace("{EventOrganizerLastName}", canceledEvent.Organizer.LastName);

                emailBody.Replace("{UserFirstName}", user.FirstName);
                emailBody.Replace("{UserLastName}", user.LastName);

                var email = new Email.Email();

                email.Recipients.Add(user.EMail);
                email.Subject = template.Subject;
                email.Body = emailBody;

                emails.Add(email);
            }

            MailService.SendMessages(emails);
        }

        public void SendPositionFilledNotification(VolunteerPosition position)
        {
            var template = uow.NotificationTemplates
                .Get()
                .FirstOrDefault(nt => nt.Id == (int)NotificationTypes.PositionFilled);

            var emailBody = template.Template;

            emailBody.Replace("{FirstName}", position.Event.Organizer.FirstName);
            emailBody.Replace("{LastName}", position.Event.Organizer.LastName);

            emailBody.Replace("{PositionName}", position.Name);
            emailBody.Replace("{EventName}", position.Event.Name);
            emailBody.Replace("{EventType}", position.Event.EventType.Name);
            emailBody.Replace("{EventLocation}", position.Event.Location.Name);

            emailBody.Replace("{EventStartDate}", position.Event.StartDate.Date.ToString());
            emailBody.Replace("{EventEndDate}", position.Event.EndDate.Date.ToString());

            var email = new Email.Email();

            email.Body = emailBody;
            email.Recipients.Add(position.Event.Organizer.EMail);
            email.Subject = template.Subject;

            MailService.SendMessage(email);
        }

        public void SendEventFilledNotification(Event filledEvent)
        {
            var template = uow.NotificationTemplates
                .Get()
                .FirstOrDefault(nt => nt.Id == (int)NotificationTypes.EventFilled);

            var emailBody = template.Template;

            emailBody.Replace("{FirstName}", filledEvent.Organizer.FirstName);
            emailBody.Replace("{LastName}", filledEvent.Organizer.LastName);

            emailBody.Replace("{EventName}", filledEvent.Name);
            emailBody.Replace("{EventType}", filledEvent.EventType.Name);
            emailBody.Replace("{EventLocation}", filledEvent.Location.Name);

            emailBody.Replace("{EventStartDate}", filledEvent.StartDate.Date.ToString());
            emailBody.Replace("{EventEndDate}", filledEvent.EndDate.Date.ToString());

            var email = new Email.Email();

            email.Body = emailBody;
            email.Recipients.Add(filledEvent.Organizer.EMail);
            email.Subject = template.Subject;

            MailService.SendMessage(email);
        }
    }
}
