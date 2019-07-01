import tl = require('./node_modules/azure-pipelines-task-lib/task');
import url = require('url');
const https = require('https');

async function run() {
    try {
        const connection = tl.getInput('EsquioService', true);
        const flagId = Number.parseInt(tl.getInput('flagId', true));
        const esquioUrl = url.parse(tl.getEndpointUrl(connection, false));
        const apikey = tl.getEndpointDataParameter(connection, 'apiKey', true);

        await rolloutFeature(esquioUrl, apikey, flagId)
    }
    catch (err) {
        tl.setResult(tl.TaskResult.Failed, err.message);
    }
}

async function rolloutFeature(esquioUrl: url.UrlWithStringQuery, esquioApiKey: string, flagId: number) {
    const options = {
        hostname: esquioUrl.host,
        path: `/api/v1/flags/${flagId}/Rollout?apikey=${esquioApiKey}`,
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        }
    }
    const req = https.request(options, (res: any) => {
        if(res.statusCode === 200){
            console.log('Feature rolled out successfully');
        }
    });
    req.on('error', (error: any) => {
        console.error('There has been an error rolling out feature');
    });
    
    req.end();
}

run();