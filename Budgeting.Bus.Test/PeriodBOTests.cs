using Budgeting.Core.Exceptions;
using Budgeting.Entity;
using Budgeting.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Budgeting.Bus.Test
{
    [TestClass]
    public class PeriodBOTests
    {
        private Mock<ModelContext> _mockContext;

        private void InitialiseData()
        {
            var data = new List<Period>
            {
                new Period { PeriodId = 1, Title  = "First Period", StartDate = DateTime.Parse("01 July 2016"), StartingBalance = 100, CreatedDate = DateTime.Parse("01 June 2016") },
                new Period { PeriodId = 2, Title  = "Second Period", StartDate = DateTime.Parse("01 August 2016"), StartingBalance = 200, CreatedDate = DateTime.Parse("01 May 2016") },
                new Period { PeriodId = 3, Title  = "Third Period", StartDate = DateTime.Parse("01 September 2016"), StartingBalance = 200, CreatedDate = DateTime.Parse("01 April 2016"), ArchivedDate = DateTime.Parse("02 April 2016") }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Period>>();
            mockSet.As<IQueryable<Period>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Period>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Period>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Period>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext = new Mock<ModelContext>();
            _mockContext.Setup(c => c.Periods).Returns(mockSet.Object);
        }

        [TestMethod]
        public void PeriodBO_Constructor_CreatesObject()
        {
            InitialiseData();

            var period = new PeriodBO(_mockContext.Object, 1);

            Assert.AreEqual(1, period.PeriodId, "period.PeriodId");
            Assert.AreEqual("First Period", period.Title, "period.Title");
            Assert.AreEqual(DateTime.Parse("01 July 2016"), period.StartDate, "period.StartDate");
            Assert.AreEqual(100, period.StartingBalance, "period.StartingBalance");
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public void PeriodBO_Constructor_ThrowsObjectNotFoundException()
        {
            InitialiseData();

            var period = new PeriodBO(_mockContext.Object, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException))]
        public void PeriodBO_Constructor_ThrowsObjectNotFoundExceptionWithViewModel()
        {
            InitialiseData();
            var periodVM = new PeriodViewModel()
            {
                PeriodId = 4,
                Title = "New Period",
                StartingBalance = 200,
                StartDate = DateTime.Parse("01 September 2016")
            };

            var period = new PeriodBO(_mockContext.Object, periodVM);
        }

        [TestMethod]
        public void PeriodBO_Constructor_CreatesNewObjectWithViewModel()
        {
            InitialiseData();
            var periodVM = new PeriodViewModel()
            {
                PeriodId = 0,
                Title = "Third Period",
                StartingBalance = 200,
                StartDate = DateTime.Parse("01 September 2016"),
                UserAccountId = 1
            };

            var period = new PeriodBO(_mockContext.Object, periodVM);

            Assert.AreEqual(0, period.PeriodId, "period.PeriodId");
            Assert.AreEqual("Third Period", period.Title, "period.Title");
            Assert.AreEqual(DateTime.Parse("01 September 2016"), period.StartDate, "period.StartDate");
            Assert.AreEqual(200, period.StartingBalance, "period.StartingBalance");
            Assert.AreEqual(1, period.UserAccountId, "period.UserAccountId");
            Assert.AreEqual(period.CreatedDate, DateTime.MinValue, "period.CreatedDate is MinValue");

            //Test Save method updates CreatedDate
            period.Save();

            Assert.AreNotEqual(period.CreatedDate, DateTime.MinValue, "period.CreatedDate is now");
        }

        [TestMethod]
        public void PeriodBO_Constructor_CreatesExistingObjectWithViewModel()
        {
            InitialiseData();
            var periodVM = new PeriodViewModel()
            {
                PeriodId = 1,
                Title = "First Period Update",
                StartingBalance = 200,
                StartDate = DateTime.Parse("01 October 2016"),
                UserAccountId = 1
            };

            var period = new PeriodBO(_mockContext.Object, periodVM);

            Assert.AreEqual(1, period.PeriodId, "period.PeriodId");
            Assert.AreEqual("First Period Update", period.Title, "period.Title");
            Assert.AreEqual(DateTime.Parse("01 October 2016"), period.StartDate, "period.StartDate");
            Assert.AreEqual(200, period.StartingBalance, "period.StartingBalance");
            Assert.AreEqual(1, period.UserAccountId, "period.UserAccountId");
            Assert.AreEqual(DateTime.Parse("01 June 2016"), period.CreatedDate, "period.CreatedDate");
            Assert.IsNull(period.LastModifiedByDate, "period.LastModifiedByDate is null");

            //Test Save method updates LastModifiedDate
            period.Save();

            Assert.IsNotNull(period.LastModifiedByDate, "period.LastModifiedByDate is not null");
        }

        [TestMethod]
        public void PeriodBO_Archive_SetsArchivedDate()
        {
            InitialiseData();

            var period = new PeriodBO(_mockContext.Object, 2);

            period.Archive();

            Assert.IsNotNull(period.ArchivedDate, "period.ArchivedDate is not null");
            Assert.AreNotEqual(DateTime.MinValue, period.ArchivedDate, "period.ArchivedDate is not minvalue");
        }

        [TestMethod]
        public void PeriodBO_Archive_RemovesArchivedDate()
        {
            InitialiseData();

            var period = new PeriodBO(_mockContext.Object, 3);

            period.Unarchive();

            Assert.IsNull(period.ArchivedDate, "period.ArchivedDate is not null");
        }
    }
}