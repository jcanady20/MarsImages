#### Mars Images

![Build Status](https://api.travis-ci.com/jcanady20/MarsImages.svg?branch=master)

[Travis](https://travis-ci.com/jcanady20/MarsImages)

Mars Images provides a service to pull photos from the NASA [Mars Photo API](https://api.nasa.gov/). Images are pulled based on dates provided via data.txt file. Each date provided will pull the first set of images for that given date.

#### Application Settings
The appsettings.json file exposes the following settings.

NASA_Api_Key: This value is an API key provided by the NASA api itself.  
Pre_Cache_File: Specifies the filename to use for provided date inputs.  
ImageStorage: Specifies a location to store cached images.  


#### Docker build
Building the application locally with docker can be achieved with the following command. This build requires a build-arg 'build_version' to be provided.
````
docker build -t marsimages:1.0 . --build-arg build_version='1.0.0.1'
````

#### Running the Container
````
docker run \
 --name=mars_images \
 --network bridge \
 -p 5002:5002 \
 -e ASPNETCORE_ENVIRONMENT='Production' \
 -e ASPNETCORE_URLS='http://+:5002'
marsimages:1.0
````
Once the container is running the application can be accessed from the exposed port. i.e. http://localhost:5002