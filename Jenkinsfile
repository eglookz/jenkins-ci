pipeline {
    agent none
    environment {
		REPO     = 'https://github.com/eglookz/jenkins-ci.git'
		SLN_NAME = '${env.WORKSPACE}\\AsposeTestApp.sln'

		NUGET = "\"C:\\Jenkins\\workspace\\tools\\nuget\\nuget.exe\""
    }

    stages {
        stage('Preparation') {
        	parallel {
                stage('Prepare on master') {
                	agent {
                        label 'master'
                    }
                    steps {
                    	checkout scm
                    	bat("${NUGET} restore \"${SLN_NAME}\"")
                    }
                }
                stage('Prepare on specific-slave') {
                	agent {
                        label 'specific'
                    }
                    steps {
                    	checkout scm
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
                    	bat("${NUNIT} \"${env.WORKSPACE}\\AsposeTestApp\\bin\\Release\\AsposeTestApp.exe\" /framework:net-4.5")
                    }
                }
                stage('Test on specific-slave') {
                	agent {
                        label 'specific'
                    }
                    steps {
                    	bat("${NUNIT} \"${env.WORKSPACE}\\AsposeTestApp\\bin\\Release\\AsposeTestApp.exe\" /framework:net-4.5")
                    }
                }
            }
		}
    }
}
