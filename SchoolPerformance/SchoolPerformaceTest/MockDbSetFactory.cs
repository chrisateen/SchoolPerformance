using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;

namespace SchoolPerformaceTest
{
    public static class MockDbSetFactory
    {
        public static Mock<DbSet<T>> setupDbSet<T>(this IQueryable<T> source) where T : class
        {
            var mockDbSet = new Mock<DbSet<T>>();

            mockDbSet.As<IQueryable<T>>()
                .Setup(x => x.Provider)
                .Returns(source.Provider);

            mockDbSet.As<IQueryable<T>>()
                .Setup(x => x.Expression)
                .Returns(source.Expression);

            mockDbSet.As<IQueryable<T>>()
                .Setup(x => x.ElementType)
                .Returns(source.ElementType);

            mockDbSet.As<IQueryable<T>>()
                .Setup(x => x.GetEnumerator())
                .Returns(source.GetEnumerator());

            return mockDbSet;
        }

    }
}
