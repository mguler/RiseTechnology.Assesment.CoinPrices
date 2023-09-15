using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq;
using RiseTechnology.Assesment.CoinPrices.Data.Tests;
using RiseTechnology.Assesment.CoinPrices.Data.Tests.Model;

namespace RiseTechnology.Assesment.CoinPrices.Data.Tests
{
    [TestClass]
    public class DataRepositoryTests
    {
        [TestMethod("Should insert the record if the model has default value on it's primary key property")]
        public void ShouldInsertIfTheModelPrimaryKeyHasDefaultValue()
        {
            var propertyGetterSetup = new Mock<IClrPropertyGetter>();
            var propertySetup = new Mock<IProperty>();
            var keySetup = new Mock<IKey>();
            var entityTypeSetup = new Mock<IEntityType>();
            var dbModelSetup = new Mock<IModel>();
            var dbContextSetup = new Mock<DbContext>();

            propertyGetterSetup.Setup(m => m.HasDefaultValue(It.IsAny<object>())).Returns(true);
            propertySetup.Setup(m => m.GetGetter()).Returns(propertyGetterSetup.Object);
            var readonlyList = new List<IProperty>() { propertySetup.Object }.AsReadOnly();
            keySetup.SetupGet(m => m.Properties).Returns(readonlyList);
            entityTypeSetup.Setup(m => m.FindPrimaryKey()).Returns(keySetup.Object);
            entityTypeSetup.SetupGet(m => m.ClrType).Returns(typeof(TestModel));
            var entityTypes = new List<IEntityType>() { entityTypeSetup.Object };
            dbModelSetup.Setup(m => m.GetEntityTypes()).Returns(entityTypes);
            dbContextSetup.Setup(m => m.Model).Returns(dbModelSetup.Object);

            var result = false;
            dbContextSetup.Setup(m => m.Add(It.IsAny<TestModel>())).Callback(() => result = true);
            var dataRepository = new DataRepositoryDefaultImpl(dbContextSetup.Object);
            dataRepository.Save(new TestModel());

            Assert.IsTrue(result);
        }

        [TestMethod("Should update the record if the model does not have default value on it's primary key property")]
        public void ShouldUpdateIfTheModelHasAPrimaryKeyValue()
        {
            var propertyGetterSetup = new Mock<IClrPropertyGetter>();
            var propertySetup = new Mock<IProperty>();
            var keySetup = new Mock<IKey>();
            var entityTypeSetup = new Mock<IEntityType>();
            var dbModelSetup = new Mock<IModel>();
            var dbContextSetup = new Mock<DbContext>();

            propertyGetterSetup.Setup(m => m.HasDefaultValue(It.IsAny<object>())).Returns(false);
            propertySetup.Setup(m => m.GetGetter()).Returns(propertyGetterSetup.Object);
            var readonlyList = new List<IProperty>() { propertySetup.Object }.AsReadOnly();
            keySetup.SetupGet(m => m.Properties).Returns(readonlyList);
            entityTypeSetup.Setup(m => m.FindPrimaryKey()).Returns(keySetup.Object);
            entityTypeSetup.SetupGet(m => m.ClrType).Returns(typeof(TestModel));
            var entityTypes = new List<IEntityType>() { entityTypeSetup.Object };
            dbModelSetup.Setup(m => m.GetEntityTypes()).Returns(entityTypes);
            dbContextSetup.Setup(m => m.Model).Returns(dbModelSetup.Object);

            var result = false;
            dbContextSetup.Setup(m => m.Update(It.IsAny<TestModel>())).Callback(() => result = true);
            var dataRepository = new DataRepositoryDefaultImpl(dbContextSetup.Object);
            dataRepository.Save(new TestModel());

            Assert.IsTrue(result);
        }

        [TestMethod("Should throw an exception if the model does not have a defined primary key property")]
        public void ShouldThrowExceptionIfModelDoesNotHavePrimaryKey()
        {
            var propertySetup = new Mock<IProperty>();
            var keySetup = new Mock<IKey>();
            var entityTypeSetup = new Mock<IEntityType>();
            var dbModelSetup = new Mock<IModel>();
            var dbContextSetup = new Mock<DbContext>();

            propertySetup.Setup(m => m.GetGetter()).Returns(() => null);
            var readonlyList = new List<IProperty>() { propertySetup.Object }.AsReadOnly();
            keySetup.SetupGet(m => m.Properties).Returns(readonlyList);
            entityTypeSetup.Setup(m => m.FindPrimaryKey()).Returns(keySetup.Object);
            entityTypeSetup.SetupGet(m => m.ClrType).Returns(typeof(TestModel));
            var entityTypes = new List<IEntityType>() { entityTypeSetup.Object };
            dbModelSetup.Setup(m => m.GetEntityTypes()).Returns(entityTypes);
            dbContextSetup.Setup(m => m.Model).Returns(dbModelSetup.Object);

            var dataRepository = new DataRepositoryDefaultImpl(dbContextSetup.Object);
            Assert.ThrowsException<Exception>(() => dataRepository.Save(new TestModel()));
        }
    }
}