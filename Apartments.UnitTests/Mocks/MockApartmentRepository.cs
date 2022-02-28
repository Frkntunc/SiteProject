using Moq;
using Site.Application.Contracts.Persistence.Repositories.Apartments;
using Site.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Apartments.UnitTests.Mocks
{
    public static class MockApartmentRepository
    {
        public static Mock<IApartmentRepository> GetApartmentRepository()
        {
            var apartmentList = new List<Apartment>()
            {
                new Apartment() {ID=1,Blok=1,ApartmentNumber=1,Floor=1,Owner="Furkan",Type="2+1",UserId=1,Status="Dolu"},
                new Apartment() {ID=2,Blok=2,ApartmentNumber=2,Floor=2,Owner="Ali",Type="2+1",UserId=2,Status="Dolu"},
                new Apartment() {ID=3,Blok=3,ApartmentNumber=3,Floor=3,Owner="Veli",Type="2+1",UserId=3,Status="Dolu"}
            };

            var mockRepo = new Mock<IApartmentRepository>();

            mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(apartmentList);

            mockRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => apartmentList.SingleOrDefault(a => a.ID == i));

            mockRepo.Setup(x => x.AddAsync(It.IsAny<Apartment>())).Callback((Apartment apartment) =>
              {
                  apartmentList.Add(apartment);
              });

            mockRepo.Setup(x => x.UpdateAsync(It.IsAny<Apartment>())).Callback((Apartment apartment) =>
              {
                  var org = apartmentList.Where(a => a.ID == apartment.ID).Single();

                  if (org == null)
                      throw new InvalidOperationException();

                  org.ApartmentNumber = apartment.ApartmentNumber;
                  org.Blok = apartment.Blok;
                  org.Floor = apartment.Floor;
                  org.Owner = apartment.Owner;
                  org.Status = apartment.Status;
                  org.Type = apartment.Type;
                  org.UserId = apartment.UserId;
              });

            return mockRepo;
        }
    }
}
