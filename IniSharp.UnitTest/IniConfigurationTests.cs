namespace IniSharp.UnitTest
{
    public class ConfigurationIntegrationTests
    {
        private readonly string _testFilePath = "@C:\test\testConfig.ini";

        public ConfigurationIntegrationTests()
        {
            // Create a sample ini file for testing
            File.WriteAllLines(_testFilePath, new string[]
            {
                "[Settings]",
                "Theme=Dark",
                "AutoSave=True",
                "",
                "[Profile]",
                "Name=John",
                "Age=30"
            });
        }

        [Fact]
        public void CanReadValues()
        {
            IConfiguration config = new Configuration(_testFilePath);

            Assert.Equal("Dark", config.GetValue("Settings:Theme"));
            Assert.Equal("True", config.GetValue("Settings:AutoSave"));
            Assert.Equal("John", config.GetValue("Profile:Name"));
            Assert.Equal("30", config.GetValue("Profile:Age"));
        }

        [Fact]
        public void CanUseIndexerToGetValues()
        {
            IConfiguration config = new Configuration(_testFilePath);

            Assert.Equal("Dark", config["Settings:Theme"]);
        }

        [Fact]
        public void CanUseIndexerToSetValues()
        {
            var config = new Configuration(_testFilePath);
            config["Settings:Theme"] = "Light";
            config.SaveChanges(); // Ensuring changes are saved after modification

            // Read the file directly to confirm persistence
            var contents = File.ReadAllText(_testFilePath);
            Assert.Contains("Theme=Light", contents);
        }

        [Fact]
        public void CanBindToStrongType()
        {
            var config = new Configuration(_testFilePath);

            var profile = config.GetSection<Profile>("Profile");
            Assert.NotNull(profile);
            Assert.Equal("John", profile.Name);
            Assert.Equal(30, profile.Age);
        }
    }

    public class Profile
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
