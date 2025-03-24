using TeamSpace.Domain.Entities;

namespace TeamSpace.Application.Test.Customizations;

public class UserCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<User>(composer => composer
            .Without(u => u.Spaces)
            .Without(u => u.Notes)
            .Without(u => u.SpaceUserRelations)
            .Without(u => u.Role));
    }
}