import os
from datetime import datetime
import urllib.request

# URL and user agent, stored in Lambda environment variables.
URL = os.environ['url']
USER_AGENT = os.environ['user_agent']

# Lambda handler that will be called during lambda execution.
def lambda_handler(event, context):
    print('Checking {} at {}...'.format(URL, event['time']))
    try:
        req = urllib.request.Request(URL,data=None,headers={'User-Agent': USER_AGENT})
        res = urllib.request.urlopen(req)
        if (res.status != 200):
            raise Exception('Call failed')
    except:
        print('Connection failed!')
        raise
    else:
        print('Connection succeeded!')
        return event['time']
    finally:
        print('Completed at {}'.format(str(datetime.now())))