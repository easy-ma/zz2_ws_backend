using System;
using Xunit;
using Moq;

using turradgiver_dal.Models;
using turradgiver_dal.Repositories;

namespace turradgiver_test
{
    public class RepositoryTest
    {
        [Fact]
        public void Test1()
        {
            var testUser = new User("Bilel","email") { Id = new Guid("b4ae46e1-41a3-49a2-bf59-76ced244cd30")};
            var context = new Mock<DbContext>();
            var dbSetMock = new Mock<DbSet<User>>();
            context.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).Returns(testObject);

            // Act
            var repository = new Repository<User>(context.Object);
            repository.GetByIdAsync(new Guid("b4ae46e1-41a3-49a2-bf59-76ced244cd30"));

            // Assert
            context.Verify(x => x.Set<TestClass>());
            dbSetMock.Verify(x => x.Find(It.IsAny<Guid>()));
    //     //Assert
    //     context.Verify(x => x.Set<TestClass>());
    //     dbSetMock.Verify(x => x.Add(It.Is<TestClass>(y => y == testObject)));
        }

        // Task<IQueryable<T>> GetAllAsync();
        // Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression);
        // Task<T> GetByIdAsync(Guid id);
        // Task CreateAsync(T entity);
        // Task UpdateAsync(T entity);
        // Task DeleteAsync(T entity);
        // Task DeleteByIdAsync(Guid id);
        // Task<IQueryable<T>> GetByRangeAsync(int skip, int number);
        // Task<IQueryable<T>> GetByRangeAsync(int skip, int number, Expression<Func<T, bool>> include);

        // Task<IQueryable<T>> IncludeAsync(Expression<Func<T,object>> include);
    }
}
