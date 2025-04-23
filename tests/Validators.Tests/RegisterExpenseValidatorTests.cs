using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
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


    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    [InlineData(null)]
    public void Error_Title_Empty(string title)
    {
        // arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = title;
        
        // act
        var result = validator.Validate(request);

        // assert
        result.IsValid.ShouldBeFalse(); // usando biblioteca Shouldly
        
        // Garante que só exista UM erro retornado
        result.Errors.ShouldHaveSingleItem();

        // E que esse erro seja o do título
        result.Errors.Single().ErrorMessage.ShouldBe(ResourceErrorMessages.TITLE_REQUIRED);
    }


    [Fact]
    public void Error_Date_Future()
    {
        // arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);

        // act
        var result = validator.Validate(request);

        // assert
        result.IsValid.ShouldBeFalse(); // usando biblioteca Shouldly

        // Garante que só exista UM erro retornado
        result.Errors.ShouldHaveSingleItem();

        // E que esse erro seja o do título
        result.Errors.Single().ErrorMessage.ShouldBe(ResourceErrorMessages.EXPENSES_CANNOT_FOR_THE_FUTURE);
    }


    [Fact]
    public void Error_Payment_Type_Invalid()
    {
        // arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.PaymentType = (PaymentType)700;

        // act
        var result = validator.Validate(request);

        // assert
        result.IsValid.ShouldBeFalse(); // usando biblioteca Shouldly

        // Garante que só exista UM erro retornado
        result.Errors.ShouldHaveSingleItem();

        // E que esse erro seja o do título
        result.Errors.Single().ErrorMessage.ShouldBe(ResourceErrorMessages.PAYMENT_TYPE_INVALID);
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Error_Amount_Invalid(decimal amount)
    {
        // arrange
        var validator = new RegisterExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Amount = amount;

        // act
        var result = validator.Validate(request);

        // assert
        result.IsValid.ShouldBeFalse(); // usando biblioteca Shouldly

        // Garante que só exista UM erro retornado
        result.Errors.ShouldHaveSingleItem();

        // E que esse erro seja o do título
        result.Errors.Single().ErrorMessage.ShouldBe(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
    }
}
