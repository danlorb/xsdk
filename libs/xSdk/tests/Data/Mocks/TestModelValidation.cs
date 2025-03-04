using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSdk.Data.Mocks
{
    public class TestModelValidation : AbstractValidator<TestModel>
    {
        public TestModelValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
