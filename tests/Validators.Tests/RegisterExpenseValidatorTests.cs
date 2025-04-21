using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CommonTestUtilities.Requests;

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
        Assert.True(result.IsValid);
    }
}
