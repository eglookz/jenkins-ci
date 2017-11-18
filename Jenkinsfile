pipeline {
    agent none
    environment {
		SLN_NAME = 'AsposeTestApp.sln'
		REPO     = 'https://github.com/eglookz/jenkins-ci.git'
		
		NUGET    = "\"C:\\Jenkins\\workspace\\tools\\nuget\\nuget.exe\""
		NUNIT    = "\"C:\\Program Files (x86)\\NUnit.org\\nunit-console\\nunit3-console.exe\""
    }

    stages {
        stage('Preparation') {
        	parallel {
                stage('Prepare on master') {
                	agent {
                        label 'master'
                    }
                    steps {
                    	git url: REPO
                    	bat("${NUGET} restore \"${SLN_NAME}\"")
                    }
                }
                stage('Prepare on specific-slave') {
                	agent {
                        label 'specific'
                    }
                    steps {
                    	git url: REPO
                    	bat("${NUGET} restore \"${SLN_NAME}\"")
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
                    	bat "${tool 'MSBuild'} ${SLN_NAME} /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
                    }
                }
                stage('Build on specific-slave') {
                	agent {
                        label 'specific'
                    }
                    steps {
                    	bat "${tool 'MSBuild'} ${SLN_NAME} /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
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
                    	bat("${NUNIT} \"AsposeTestApp\\bin\\Release\\AsposeTestApp.exe\" /framework:net-4.5")
                    }
                }
                stage('Test on specific-slave') {
                	agent {
                        label 'specific'
                    }
                    steps {
                    	bat("${NUNIT} \"AsposeTestApp\\bin\\Release\\AsposeTestApp.exe\" /framework:net-4.5")
                    }
                }
            }
		}
    }
}
