using System.Collections.Generic;

namespace Policy.Contracts.Responses
{
    public class CliResponseDto
    {
        public CliResponseDto(IEnumerable<string> output, bool responseRequired = false)
        {
            Output = output;
            ResponseRequired = responseRequired;
        }

        public bool ResponseRequired { get; set; }

        public IEnumerable<string> Output { get; set; }
    }
}