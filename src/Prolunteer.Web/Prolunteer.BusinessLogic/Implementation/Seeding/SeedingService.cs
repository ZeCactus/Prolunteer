using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Prolunteer.BusinessLogic.Base;
using Prolunteer.BusinessLogic.Implementation.Account.Utilities;
using Prolunteer.BusinessLogic.Implementation.Certification;
using Prolunteer.BusinessLogic.Implementation.Event;
using Prolunteer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Prolunteer.BusinessLogic.Implementation.Seeding
{
    class UserIdModel
    {
        public Guid Id { get; set; }
    }

    public static class ListShuffler
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            for(var i = 0; i < 1000; i++)
            {
                int n = list.Count;
                var rng = new Random();
                while (n > 1)
                {
                    n--;
                    int k = rng.Next(n + 1);
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }
        }
    }
    public class SeedingService : BaseService
    {
        private readonly EventService EventService;

        public SeedingService(ServiceDependencies dependencies, EventService eventService)
            : base(dependencies)
        {
            this.EventService = eventService;
        }

        public void TestEFCore()
        {
            var county = new Entities.County
            {
                Id = Guid.NewGuid(),
                Name = "TestareaJudet"
            };

            uow.Counties.Insert(county);
            uow.Counties.Update(county);

            uow.SaveChanges();
        }

        public void SeedUsers()
        {
            List<string> firstNames;
            using (var reader = new StreamReader("C:\\Users\\bogdan.paicu\\Source\\Repos\\BogdanPaicu\\src\\Prolunteer.Web\\Prolunteer.BusinessLogic\\Implementation\\Seeding\\Files\\firstnames.json"))
            {
                var json = reader.ReadToEnd();
                firstNames = JsonConvert.DeserializeObject<List<string>>(json);
            }

            List<string> lastNames;
            using (var reader = new StreamReader("C:\\Users\\bogdan.paicu\\Source\\Repos\\BogdanPaicu\\src\\Prolunteer.Web\\Prolunteer.BusinessLogic\\Implementation\\Seeding\\Files\\lastnames.json"))
            {
                var json = reader.ReadToEnd();
                lastNames = JsonConvert.DeserializeObject<List<string>>(json);
            }

            ExecuteInTransaction(uow =>
            {

                var VolunteerRole = new Entities.Role
                {
                    Name = Enum.GetName(typeof(RoleTypes), (int)RoleTypes.Volunteer)
                };

                var EventManagerRole = new Entities.Role
                {
                    Name = Enum.GetName(typeof(RoleTypes), (int)RoleTypes.EventManager)
                };

                var AdminRole = new Entities.Role
                {
                    Name = Enum.GetName(typeof(RoleTypes), (int)RoleTypes.Admin)
                };

                uow.Roles.Insert(VolunteerRole);
                uow.Roles.Insert(EventManagerRole);
                uow.Roles.Insert(AdminRole);

                uow.SaveChanges();
            });

            ExecuteInTransaction(uow =>
            {
                var rootAdmin = new Entities.User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Bogdan",
                    LastName = "Paicu",
                    EMail = "bogdan.paicu0@gmail.com",
                    BirthDay = new DateTime(1998, 10, 9),
                    PasswordHash = PasswordUtilities.Hash("adminpass"),
                    UserRoles = new List<Entities.UserRole> { new Entities.UserRole { RoleId = (int)RoleTypes.Admin } }
                };

                uow.Users.Insert(rootAdmin);

                var random = new Random();
                var firstNamesCount = firstNames.Count;
                var lastNamesCount = lastNames.Count;
                var userList = new List<Entities.User>();
                for (var i = 0; i < 7000; i++)
                {
                    var firstName = firstNames[random.Next(firstNamesCount)];
                    var lastName = lastNames[random.Next(lastNamesCount)];
                    var newUser = new Entities.User
                    {
                        Id = Guid.NewGuid(),
                        FirstName = firstName,
                        LastName = lastName,
                        EMail = $"{firstName}.{lastName}{i}@email.com".ToLower(),
                        BirthDay = new DateTime(random.Next(1970, 2001), random.Next(1, 12), random.Next(1, 28)),
                        PasswordHash = PasswordUtilities.Hash($"{firstName}{lastName}{i}".ToLower()),
                        UserRoles = new List<Entities.UserRole> { new Entities.UserRole { RoleId = (int)RoleTypes.Volunteer } }
                    };
                    userList.Add(newUser);
                }

                for (var i = 0; i < 2900; i++)
                {
                    var firstName = firstNames[random.Next(firstNamesCount)];
                    var lastName = lastNames[random.Next(lastNamesCount)];
                    var newUser = new Entities.User
                    {
                        Id = Guid.NewGuid(),
                        FirstName = firstName,
                        LastName = lastName,
                        EMail = $"{firstName}.{lastName}{i}@email.com".ToLower(),
                        BirthDay = new DateTime(random.Next(1970, 2001), random.Next(1, 12), random.Next(1, 28)),
                        PasswordHash = PasswordUtilities.Hash($"{firstName}{lastName}{i}".ToLower()),
                        UserRoles = new List<Entities.UserRole> { new Entities.UserRole { RoleId = (int)RoleTypes.EventManager } }
                    };
                    userList.Add(newUser);
                }

                for (var i = 0; i < 100; i++)
                {
                    var firstName = firstNames[random.Next(firstNamesCount)];
                    var lastName = lastNames[random.Next(lastNamesCount)];
                    var newUser = new Entities.User
                    {
                        Id = Guid.NewGuid(),
                        FirstName = firstName,
                        LastName = lastName,
                        EMail = $"{firstName}.{lastName}{i}@email.com".ToLower(),
                        BirthDay = new DateTime(random.Next(1970, 2001), random.Next(1, 12), random.Next(1, 28)),
                        PasswordHash = PasswordUtilities.Hash($"{firstName}{lastName}{i}".ToLower()),
                        UserRoles = new List<Entities.UserRole> { new Entities.UserRole { RoleId = (int)RoleTypes.Admin } }
                    };
                    userList.Add(newUser);
                }

                uow.Users.InsertRange(userList);
                uow.SaveChanges();
            });
        }

        public void SeedCounties()
        {
            List<string> countyNames;
            using (var reader = new StreamReader("C:\\Users\\bogdan.paicu\\Source\\Repos\\BogdanPaicu\\src\\Prolunteer.Web\\Prolunteer.BusinessLogic\\Implementation\\Seeding\\Files\\counties.json"))
            {
                var json = reader.ReadToEnd();
                countyNames = JsonConvert.DeserializeObject<List<string>>(json);
            }

            ExecuteInTransaction(uow =>
            {
                var countyList = new List<Entities.County>();
                foreach (var countyName in countyNames)
                {
                    var newCounty = new Entities.County
                    {
                        Id = Guid.NewGuid(),
                        Name = countyName
                    };
                    countyList.Add(newCounty);
                }

                uow.Counties.InsertRange(countyList);
                uow.SaveChanges();
            });
        }

        public void SeedCities()
        {
            Dictionary<string, List<string>> citiesByCounty;
            using (var reader = new StreamReader("C:\\Users\\bogdan.paicu\\Source\\Repos\\BogdanPaicu\\src\\Prolunteer.Web\\Prolunteer.BusinessLogic\\Implementation\\Seeding\\Files\\cities.json"))
            {
                var json = reader.ReadToEnd();
                citiesByCounty = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json);
            }

            ExecuteInTransaction(uow =>
            {
                var citiesToAdd = new List<Entities.City>();

                foreach (var entry in citiesByCounty)
                {
                    var countyToAddTo = uow.Counties.Get().Where(c => c.Name == entry.Key).SingleOrDefault();

                    foreach (var cityName in entry.Value)
                    {
                        var newLocations = new List<Entities.Location>();

                        if (entry.Key != "Bucuresti")
                        {
                            for (var i = 0; i < 10; i++)
                            {
                                var newLocation = new Entities.Location
                                {
                                    Id = Guid.NewGuid(),
                                    Name = $"{cityName} Location {i}",
                                    Description = $"This is a description for {cityName} Location {i}!"
                                };

                                newLocations.Add(newLocation);
                            }
                        }
                        var newCity = new Entities.City
                        {
                            Id = Guid.NewGuid(),
                            County = countyToAddTo,
                            Name = cityName,
                            Locations = newLocations
                        };

                        citiesToAdd.Add(newCity);
                    }
                }

                uow.Cities.InsertRange(citiesToAdd);
                uow.SaveChanges();
            });
        }

        public void SeedCertifications()
        {
            ExecuteInTransaction(uow =>
            {
                var certificationsToAdd = new List<Entities.Certification>();

                var languages = new List<string> { "French", "German", "Swedish", "Danish",
                                                   "Greek", "Bulgarian", "Finnish", "Ukrainian",
                                                   "Spanish", "Romanian", "Croatian", "Czech",
                                                   "Latvian", "Maltese", "Portuguese", "Russian",
                                                   "Italian", "Polish"};

                foreach (var language in languages)
                {
                    certificationsToAdd.Add(new Entities.Certification
                    {
                        Name = $"{language} Certification",
                        Description = $"Certifies that the volunteer is fluent in {language}"
                    });
                }

                certificationsToAdd.Add(new Entities.Certification
                {
                    Name = "First Aid Certification",
                    Description = "Certifies that the volunteer can administer first aid"
                });

                certificationsToAdd.Add(new Entities.Certification
                {
                    Name = "Food Handling Certification",
                    Description = "Certifies that the volunteer can safely handle food for a large number of people"
                });

                certificationsToAdd.Add(new Entities.Certification
                {
                    Name = "Educator Certification",
                    Description = "Certifies that the volunteer is able to help with education and training of people"
                });

                certificationsToAdd.Add(new Entities.Certification
                {
                    Name = "Animal Handling Certification",
                    Description = "Certifies that the volunteer is capable of handling animals safely"
                });

                certificationsToAdd.Add(new Entities.Certification
                {
                    Name = "Photography Certification",
                    Description = "Certifies that the volunteer is professionally skilled in photography"
                });

                certificationsToAdd.Add(new Entities.Certification
                {
                    Name = "Team Leader Certification",
                    Description = "Certifies that the volunteer is able to lead and coordinate a team"
                });

                certificationsToAdd.Add(new Entities.Certification
                {
                    Name = "ASL Certification",
                    Description = "Certifies that the volunteer is fluent in American Sign Language"
                });

                certificationsToAdd.Add(new Entities.Certification
                {
                    Name = "PR Certifications",
                    Description = "Certifies that the volunteer is able to handle public relations for a public person"
                });

                certificationsToAdd.Shuffle();

                uow.Certifications.InsertRange(certificationsToAdd);
                uow.SaveChanges();
            });
        }

        public void SeedEventTypes()
        {
            ExecuteInTransaction(uow =>
            {
                var eventTypesToAdd = new List<Entities.EventType>();

                eventTypesToAdd.Add(new Entities.EventType
                {
                    Name = "Fundraiser",
                    Description = "Event for fundraising"
                });

                eventTypesToAdd.Add(new Entities.EventType
                {
                    Name = "Tutoring",
                    Description = "Event for tutoring"
                });

                eventTypesToAdd.Add(new Entities.EventType
                {
                    Name = "Concert",
                    Description = "Concert event"
                });

                eventTypesToAdd.Add(new Entities.EventType
                {
                    Name = "Press conference",
                    Description = "Event for a press conference"
                });

                eventTypesToAdd.Add(new Entities.EventType
                {
                    Name = "Festival",
                    Description = "Event for a festival"
                });

                eventTypesToAdd.Add(new Entities.EventType
                {
                    Name = "Community Service",
                    Description = "Event that helps the local community"
                });

                eventTypesToAdd.Add(new Entities.EventType
                {
                    Name = "Treasure Hunt",
                    Description = "A treasure hunt type event"
                });

                eventTypesToAdd.Add(new Entities.EventType
                {
                    Name = "Race",
                    Description = "A race type event"
                });

                eventTypesToAdd.Add(new Entities.EventType
                {
                    Name = "Vaccination Drive",
                    Description = "An event for vaccinating the public"
                });

                eventTypesToAdd.Add(new Entities.EventType
                {
                    Name = "Blood Drive",
                    Description = "An event to collect blood donations"
                });

                uow.EventTypes.InsertRange(eventTypesToAdd);
                uow.SaveChanges();
            });
        }

        private List<UserIdModel> GetVolunteerIds()
        {
            return ExecuteInTransaction(uow =>
            {
                return uow.Users.Get().Where(u => u.UserRoles.Any(ur => ur.RoleId == (int)RoleTypes.Volunteer)).Select(u => new UserIdModel { Id = u.Id }).ToList();
            });
        }

        public void SeedUserCertifications()
        {
            var users = uow.Users.Get().Where(u => u.UserRoles.Any(ur => ur.RoleId == (int)RoleTypes.Volunteer)).ToList();
            var loremIpsumDocument = File.ReadAllBytes("C:\\Users\\bogdan.paicu\\Source\\Repos\\BogdanPaicu\\src\\Prolunteer.Web\\Prolunteer.BusinessLogic\\Implementation\\Seeding\\Files\\lorem.pdf");
            var rng = new Random();
            foreach (var user in users)
            {
                var certificationsToAdd = new List<Entities.UserCertification>();
                var certificationAmount = rng.Next(1, 3);
                for (var i = 0; i < certificationAmount; i++)
                {
                    var availableCertificationsQuery = uow.Certifications
                                                .Get()
                                                .AsNoTracking()
                                                .Where(c => !c.UserCertifications
                                                                .Any(uc => uc.UserId == user.Id));
                    var certificationIndex = rng.Next(availableCertificationsQuery.Count());

                    var certificationToAdd = availableCertificationsQuery.Skip(certificationIndex).FirstOrDefault();
                    user.UserCertifications.Add(new Entities.UserCertification
                    {
                        CertificationId = certificationToAdd.Id,
                        Approved = rng.Next(10) < 7,
                        UserCertificationDocuments = new List<Entities.UserCertificationDocument> {
                            new Entities.UserCertificationDocument{
                                Id = Guid.NewGuid(),
                                Document = loremIpsumDocument
                            }
                        }
                    });
                    uow.SaveChanges();
                }
            }

        }

        public void SeedEvents()
        {
            ExecuteInTransaction(uow =>
            {
                var users = uow.Users
                    .Get()
                    .Where(u => u.UserRoles.Any(ur => ur.RoleId == (int)RoleTypes.EventManager))
                    .ToList();
                var rng = new Random();
                var eventsToAdd = new List<Entities.Event>();
                foreach(var user in users)
                {
                    var nrOfEventsToAdd = rng.Next(1, 3);

                    for(var i = 0; i < nrOfEventsToAdd; i++)
                    {
                        var certificationId = rng.Next(5, 30);
                        var secondCertificationId = certificationId + rng.Next(30 - certificationId);

                        var startDate = new DateTime(rng.Next(DateTime.Now.Year, DateTime.Now.Year + 1)
                                                    ,rng.Next(DateTime.Now.Month, 12)
                                                    ,rng.Next(DateTime.Now.Day + 1, 28));


                        var location = uow.Locations.Get().AsNoTracking().Skip(rng.Next(1, 3140)).FirstOrDefault();

                        var eventToAdd = new Entities.Event
                        {
                            Id = Guid.NewGuid(),
                            OrganizerId = user.Id,
                            EventTypeId = rng.Next(2, 11),
                            LocationId = location.Id,
                            Name = $"{user.FirstName}'s E{i+1}",
                            Description = $"Event nr {i+1} of {user.FirstName} {user.LastName}",
                            StartDate = startDate,
                            EndDate = startDate.AddDays(rng.Next(3, 8)),
                        };

                        for(int j = 0; j < 2; j++)
                        {
                            var positionToAdd = new Entities.VolunteerPosition
                            {
                                Id = Guid.NewGuid(),
                                Name = $"Event{i + 1} position{j + 1}",
                                Description = $"Volunteer position nr. {j + 1} of {eventToAdd.Name}",
                                MaximumNrOfVolunteers = rng.Next(5, 50),
                            };

                            eventToAdd.VolunteerPositions.Add(positionToAdd);
                        }

                        eventsToAdd.Add(eventToAdd);
                    }
                }
                uow.Events.InsertRange(eventsToAdd);
                uow.SaveChanges();
            });
        }

        public void SeedVolunteerParticipation()
        {
            var users = uow.Users.Get().Where(u => u.UserRoles.Any(ur => ur.RoleId == (int)RoleTypes.Volunteer)).ToList();

            var rng = new Random();

            var failedAttempts = 0;

            var counter = 0;

            foreach(var user in users)
            {
                var nrOfParticipationsToAdd = rng.Next(2, 5);

                for(var i = 0; i < nrOfParticipationsToAdd; i++)
                {
                    var availableEvent = EventService.GetAvailableEvent();

                    if(availableEvent == null)
                    {
                        failedAttempts++;
                        break;
                    }

                    var availablePosition = EventService.GetAvailablePosition(availableEvent.Id, user.Id);

                    if(availablePosition == null)
                    {
                        break;
                    }


                    user.VolunteerParticipations.Add(new Entities.VolunteerParticipation
                    {
                        VolunteerPosition = availablePosition
                    });
                }
                counter++;
                if(counter == 1000)
                {
                    uow.SaveChanges();
                    counter = 0;
                }
            }
            uow.SaveChanges();
        }
    }
}
