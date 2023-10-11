using System.Net;
using FluentResults;

namespace Platform.ErrorHandling.ApplicationErrors;

public class CalculationError : Error
{
    public CalculationError()
        : base($"Error while processing the load. Please try again!.")
    { 
        
        Metadata.Add(ErrorMetadataKeys.Title, "Calculation Error");
        Metadata.Add(ErrorMetadataKeys.ErrorCode, HttpStatusCode.InternalServerError);
    }
}