using ACMESchool.Domain.Exceptions;
using Xunit;
using System;

namespace ACMESchool.Tests.TestHelpers
{
    public static class TestExtensions
    {
        public static void ShouldThrow<TException>(this Action action) where TException : Exception
        {
            Assert.Throws<TException>(action);
        }

        public static void ShouldBeEqual(this object expected, object actual)
        {
            Assert.Equal(expected, actual);
        }
    }
}