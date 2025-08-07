using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestRegisterExpenseJsonBuilder
{
    public static RequestExpenseJson Build()
    {
        return new Faker<RequestExpenseJson>()
            .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.Date, faker => faker.Date.Past())
            .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentType>()) // Acessa um dos valores do enum de tipos de pagamento
            .RuleFor(r => r.Amount, faker => faker.Random.Decimal(min: 1, max: 1000)); // Gera um número aleatório entre 1 e 1000 (pode escolher qual número desejar para mínimo e máximo, ou pode também não especificar limites de máximo e mínimo)
    }
}
