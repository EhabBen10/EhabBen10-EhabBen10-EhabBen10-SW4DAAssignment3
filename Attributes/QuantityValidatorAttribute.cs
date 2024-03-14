using System.ComponentModel.DataAnnotations;

public class QuantityValidatorAttribute : ValidationAttribute
{
    public QuantityValidatorAttribute() : base("Quantity must be a non-negative value.")
    {
    }
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            if (value is int quantity && quantity < 0)
            {
                return new ValidationResult(ErrorMessage);
            }
            else if (value is decimal decimalQuantity && decimalQuantity < 0)
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success!;
    }
}