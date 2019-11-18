/*
 *
 * Copyright 2015 gRPC authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

var PROTO_PATH = __dirname + '/eventlog.proto';

var grpc = require('grpc');
var protoLoader = require('@grpc/proto-loader');
var packageDefinition = protoLoader.loadSync(
    PROTO_PATH,
    {keepCase: true,
     longs: String,
     enums: String,
     defaults: true,
     oneofs: true
    });

var hello_proto = grpc.loadPackageDefinition(packageDefinition).Log;

function main() {
  var client = new hello_proto.Logger('localhost:5000',
    grpc.credentials.createInsecure());
    var call = client.GetEventStream({});
    call.on('data', function(data) {
       console.log(data);
    });
    call.on('end', function() {
        // The server has finished sending
        console.log(`I've ended ... `);
    });
    call.on('error', function(e) {
        // An error has occurred and the stream has been closed.
        console.log(`Error: ${e}`);
    });
    call.on('status', function(status) {
        // process status
        console.log(`Status: ${status}`);
    });
}
main();
