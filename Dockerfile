# This is a temporary Dockerfile for testing Linux builds.
# The build should be called one_sdk_unity_test.x86_64 (build name in Unity - one_sdk_unity_test) and put into Build folder.

FROM ubuntu:18.04

EXPOSE 19001/tcp

COPY Build/. ./

RUN chmod +x one_sdk_unity_test.x86_64

CMD ["./one_sdk_unity_test.x86_64", "-batchmode", "-nographics", "-logfile"]