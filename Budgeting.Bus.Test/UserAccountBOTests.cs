﻿using Budgeting.Core.Exceptions;
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
    public class UserAccountBOTests
    {
        private Mock<ModelContext> _mockContext;

        private void InitialiseData()
        {
            var data = new List<UserAccount>
            {
                new UserAccount { UserAccountId = 1, UserName  = "AAA", Salt = "somesalt", Password = "yEJE9WIBlFkhUGHqV7At+g==" },
                new UserAccount { UserAccountId = 2, UserName  = "BBB", Salt = "frottage", Password = "qweqweqwe" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<UserAccount>>();
            mockSet.As<IQueryable<UserAccount>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<UserAccount>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<UserAccount>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<UserAccount>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext = new Mock<ModelContext>();
            _mockContext.Setup(c => c.UserAccounts).Returns(mockSet.Object);
        }

        [TestMethod]
        public void UserAccountBO_GetUserAccounts_Returns2()
        {
            InitialiseData();

            var accounts = UserAccountBO.GetUserAccounts(_mockContext.Object);

            Assert.AreEqual(2, accounts.Count, "GetUserAccounts not equal to 2");
            Assert.AreEqual(1, accounts[0].UserAccountId, "accounts[0].UserAccountId <> 1");
            Assert.AreEqual("AAA", accounts[0].UserName, "accounts[0].UserName <> BBB");
            Assert.AreEqual(2, accounts[1].UserAccountId, "accounts[1].UserAccountId <> 2");
            Assert.AreEqual("BBB", accounts[1].UserName, "accounts[1].UserName <> BBB");
        }

        [TestMethod]
        public void UserAccountBO_Constructor_CreatesObject()
        {
            InitialiseData();

            var account = new UserAccountBO(_mockContext.Object, "AAA", "mypassword");

            Assert.AreEqual(1, account.UserAccountId, "account.UserAccountId");
            Assert.AreEqual("AAA", account.UserName, "account.UserName");
            Assert.AreEqual(true, account.IsAuthenticated, "account.IsAuthenticated");
            Assert.AreEqual(account.CreatedDate, DateTime.MinValue, "account.CreatedDate is MinValue");
            Assert.IsNull(account.LastModifiedByDate, "account.LastModifiedByDate is null");

            //Test Save method updates CreatedDate
            account.Save();

            Assert.AreNotEqual(account.CreatedDate, DateTime.MinValue, "account.CreatedDate is now");

            //Test Save method updates LastModifiedDate
            account.Save();

            Assert.IsNotNull(account.LastModifiedByDate, "account.LastModifiedByDate is not null");
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void UserAccountBO_Constructor_ThrowsUserNotFoundException()
        {
            InitialiseData();

            var account = new UserAccountBO(_mockContext.Object, "AAA", "adifferentpassword");
        }
        
        [TestMethod]
        public void UserAccountBO_EncryptPasswordWithSalt_Test()
        {
            var result = UserAccountBO.EncryptPasswordWithSalt("mypassword", "somesalt");

            Assert.AreEqual("yEJE9WIBlFkhUGHqV7At+g==", result, "UserAccountBO.EncryptPasswordWithSalt");
        }
    }
}
