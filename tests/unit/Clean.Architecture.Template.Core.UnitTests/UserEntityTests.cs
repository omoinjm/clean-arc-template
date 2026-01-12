using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.TestCommon.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Clean.Architecture.Template.Core.UnitTests
{
    [TestClass]
    public class UserEntityTests
    {
        [TestMethod]
        public void UserEntity_Properties_CanBeSetAndGet()
        {
            // Arrange
            var user = UserFactory.CreateUser(
                id: 1,
                name: "John",
                surname: "Doe",
                email: "john.doe@example.com");

            // Assert
            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("John", user.Name);
            Assert.AreEqual("Doe", user.Surname);
            Assert.AreEqual("john.doe@example.com", user.Email);
            Assert.AreEqual("johndoe", user.Username);
            Assert.AreEqual("password", user.Password);
            Assert.AreEqual("salt", user.Salt);
            Assert.IsFalse(user.ChangePassword);
            Assert.IsFalse(user.Deleted);
            Assert.AreEqual("admin", user.CreatedBy);
            Assert.IsNull(user.UpdatedAt);
            Assert.IsNull(user.DeletedAt);
            Assert.AreEqual("123456789", user.IdNumber);
            Assert.AreEqual("1234567890", user.PhoneNumber);
            Assert.AreEqual("User", user.Role);
            Assert.IsNull(user.ForgotPasswordGuid);
            Assert.IsNull(user.Otp);
            Assert.IsTrue(user.IsActive);
            Assert.IsNull(user.LoginDate);
            Assert.AreEqual(1, user.UserStatusId);
            Assert.AreEqual("Active", user.UserStatusName);
            Assert.AreEqual("A", user.UserStatusSymbol);
            Assert.AreEqual("Green", user.UserStatusColor);
            Assert.AreEqual("token", user.Token);
            Assert.IsNull(user.ExpiryDateTime);
            Assert.IsNull(user.LoginTimeStamp);
        }
    }
}
