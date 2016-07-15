using Budgeting.Core.Exceptions;
using Budgeting.Entity;
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
                new Period { PeriodId = 1, Title  = "First Period", StartDate = DateTime.Parse("01 July 2016"), StartingBalance = 100 },
                new Period { PeriodId = 2, Title  = "Second Period", StartDate = DateTime.Parse("01 August 2016"), StartingBalance = 200 }
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
    }
}
