using System;
using System.Collections.Generic;

namespace Handbook.Pages;

internal class ValidationException : Exception
{
    public Dictionary<string, List<string>> Errors { get; }

    public ValidationException(Dictionary<string, List<string>> errors)
    {
        Errors = errors;
    }

}


