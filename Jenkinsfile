pipeline {
    agent none

    def projectRepo = 'https://github.com/eglookz/jenkins-ci.git'
	def slnName     = "${WORKSPACE}\\AsposeTestApp.sln"

	def nunitRunner = "\"${WORKSPACE}\\tools\\nunit2\\bin\\nunit-console-x86.exe\""
	def nuget       = "\"C:\\Jenkins\\workspace\\tools\\nuget\\nuget.exe\""
	def MSBuild     = "\"C:\\Program Files (x86)\\MSBuild\\14.0\\Bin\\MSBuild.exe\""
	def NUnit       = "\"C:\\Program Files (x86)\\NUnit.org\\nunit-console\\nunit3-console.exe\""

    stages {
        stage('Preparation') {
        	parallel {
                stage('Prepare on master') {
                	agent {
                        label 'master'
                    }
                    steps {
                    	checkout scm
                    	bat("${nuget} restore \"${slnName}\"")
                    }
                }
                stage('Prepare on specific-slave') {
                	agent {
                        label 'specific'
                    }
                    steps {
                    	checkout scm
                    	bat("${nuget} restore \"${slnName}\"")
                    }
                }
            }
        }
        stage('Build') {
        	parallel {
                stage('Build on master') {
                	agent {
                        label 'master'
                    }
                    steps {
                    	bat "${MSBuild} ${slnName} /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
                    }
                }
                stage('Build on specific-slave') {
                	agent {
                        label 'specific'
                    }
                    steps {
                    	bat "${MSBuild} ${slnName} /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
                    }
                }
            }			
		}
		stage('Test') {
			parallel {
                stage('Test on master') {
                	agent {
                        label 'master'
                    }
                    steps {
                    	bat("${NUnit} \"${WORKSPACE}\\AsposeTestApp\\bin\\Release\\AsposeTestApp.exe\" /framework:net-4.5")
                    }
                }
                stage('Test on specific-slave') {
                	agent {
                        label 'specific'
                    }
                    steps {
                    	bat("${NUnit} \"${WORKSPACE}\\AsposeTestApp\\bin\\Release\\AsposeTestApp.exe\" /framework:net-4.5")
                    }
                }
            }
		}
    }
}
