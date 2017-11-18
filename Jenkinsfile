node('specific-slave') {
   def slnName     = "${WORKSPACE}\\AsposeTestApp.sln"
    
   def nunitRunner = "\"${WORKSPACE}\\tools\\nunit2\\bin\\nunit-console-x86.exe\""
   def nuget       = "\"C:\\Jenkins\\workspace\\tools\\nuget\\nuget.exe\""
   def MSBuild     = "\"C:\\Program Files (x86)\\MSBuild\\14.0\\Bin\\MSBuild.exe\""
   def NUnit       = "\"C:\\Program Files (x86)\\NUnit.org\\nunit-console\\nunit3-console.exe\""
   
   stage('Preparation') {
      git url: 'https://github.com/eglookz/jenkins-ci.git'
      bat("${nuget} restore \"${slnName}\"")
   }
   stage('Build') {
      bat "${MSBuild} ${slnName} /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
   }
   stage('Test') {
      bat("${NUnit} \"${WORKSPACE}\\AsposeTestApp\\bin\\Release\\AsposeTestApp.exe\" /framework:net-4.5")
   }
}
