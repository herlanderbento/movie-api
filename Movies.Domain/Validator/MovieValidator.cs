using Movies.Domain.Entity;
using Movies.Domain.Validation;

namespace Movies.Domain.Validator;

public class MovieValidator: Validation.Validator
{
    private readonly Movie _movie;

    private const int TitleMaxLength = 255;
    private const int DescriptionMaxLength = 4_000;

    public MovieValidator(Movie movie, ValidationHandler handler) 
        : base(handler) => _movie = movie;

   
    public override void Validate()
    {
        ValidateTitle();

        if (string.IsNullOrWhiteSpace(_movie.Description))
            _handler.HandleError($"'{nameof(_movie.Description)}' is required");

        if(_movie.Description.Length > DescriptionMaxLength)
            _handler.HandleError($"'{nameof(_movie.Description)}' should be less or equal {DescriptionMaxLength} characters long");
    }
    
    private void ValidateTitle()
    {
        if (string.IsNullOrWhiteSpace(_movie.Title))
            _handler.HandleError($"'{nameof(_movie.Title)}' is required");

        if (_movie.Title.Length > 255)
            _handler.HandleError($"'{nameof(_movie.Title)}' should be less or equal {TitleMaxLength} characters long");
    }
}