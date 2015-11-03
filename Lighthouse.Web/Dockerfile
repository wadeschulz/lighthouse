FROM microsoft/aspnet:latest
MAINTAINER Wade Schulz, MD, PhD "wade.schulz@yale.edu"

# copy the build artifacts to the image
ADD /dist/ /lighthouse/

# set the working directory for subsequent commands
WORKDIR lighthouse

# fetch the NuGet dependencies for our application
RUN dnu restore

# set the working directory for subsequent commands
# expose TCP port 5004 from container
EXPOSE 5004

# Configure the image as an executable
# When the image starts it will execute the "k web" command
# effectively starting our web application
# (listening on port 5004 by default)
ENTRYPOINT ["dnx", "kestrel"]