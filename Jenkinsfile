def SLN_NAME = 'AsposeTestApp.sln'
def REPO     = 'https://github.com/eglookz/jenkins-ci.git'

def NUGET    = "\"C:\\Jenkins\\workspace\\tools\\nuget\\nuget.exe\""
def NUNIT    = "\"C:\\Program Files (x86)\\NUnit.org\\nunit-console\\nunit3-console.exe\""

def labels       = ['master', 'specific']
def prepareStage = [:]
def buildStage   = [:]
for (x in labels) {
    def label = x

    prepareStage[label] = {
        stage('Prepare on ' + label) {
            node(label) {
                git url: REPO
                bat("${NUGET} restore \"${SLN_NAME}\"")
            }
        }
    }

    buildStage[label] = {
        stage('Build on ' + label) {
            node(label) {
                bat "${tool 'MSBuild'} ${SLN_NAME} /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
            }
        }
    }
}

def testTasks = [:]
testTasks['master'] = {
    stage('Test on master') {
        node('master') {
            bat("${NUNIT} \"AsposeTestApp\\bin\\Release\\AsposeTestApp.exe\" /framework:net-4.5")
        }
    }
}
testTasks['specific'] = {
    stage('Test on specific-slave') {
        node('specific') {
            bat("${NUNIT} \"AsposeTestApp\\bin\\Release\\AsposeTestApp.exe\" /framework:net-4.5")
        }
    }
}

stage('Prepare') {
    parallel prepareStage
}

stage('Build') {
    parallel buildStage
}

stage('Test') {
    parallel testTasks
}