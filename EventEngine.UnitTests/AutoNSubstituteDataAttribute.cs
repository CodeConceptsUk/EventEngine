using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace EventEngine.UnitTests
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
            : base(new Fixture()
                .Customize(new AutoConfiguredNSubstituteCustomization()))
        {
        }
    }
}