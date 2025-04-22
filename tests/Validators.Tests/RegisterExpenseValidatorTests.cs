using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CommonTestUtilities.Requests;
using Shouldly;

namespace Validators.Tests;
public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        // arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();

        // act
        var result = validator.Validate(request);

        // assert
        result.IsValid.ShouldBeTrue(); // usando biblioteca Shouldly
        // Assert.True(result.IsValid); // método raiz do .NET
        //result.IsValid.Should().BeTrue(); // usando biblioteca FluentAssertions
    }
}
