## The assignment

- Provide 2 http endpoints (`<host>/v1/diff/<ID>/left` and `<host>/v1/diff/<ID>/right`) that accept JSON containing base64 encoded binary data on both endpoints.
- The provided data needs to be diff-ed and the results shall be available on a third endpoint (`<host>/v1/diff/<ID>`). The results shall provide the following info in JSON format:
    - If equal return that
    - If not of equal size just return that
    - If of same size provide insight in where the diff are, actual diffs are not needed.
        - So mainly offsets + length in the data

Note, that we are not looking for ideal diffing algorithm.


Make assumptions in the implementation explicit, choices are good but need to be communicated.

Use a Version Control System (preferably Git or Mercurial). Put the source code on a public repository or send it zipped.

## Must haves

- Preferably C#. If you are really not comfortable with C#, you can use some other object-oriented and/or functional language, but provide more detailed info for running the solution
- Functionality shall be under integration tests (not full code coverage is required)
- Internal logic shall be under unit tests (not full code coverage is required)
- Documentation in code
- Short readme on usage

See the sample on next site.

## Test case (input & output)
![image](https://github.com/andfilipe1/desafio-/blob/main/Screenshot_1.png?raw=true)

## API Result (in Swagger)
![image](https://github.com/andfilipe1/desafio-/blob/main/Screenshot_3.png?raw=true)
![image](https://github.com/andfilipe1/desafio-/blob/main/Screenshot_4.png?raw=true)
![image](https://github.com/andfilipe1/desafio-/blob/main/Screenshot_5.png?raw=true)


## Test Result
![image](https://raw.githubusercontent.com/andfilipe1/desafio-/main/Screenshot_2.png)

## Possible Improvements
- Improve awareness of where the differences are, perhaps providing more information than just the byte index and number of different bits.

- Data persistence. This would become a requirement if the system needed to be more failsafe. We could use a NoSql database to store the requests and their results.

- If scalability becomes a requirement, this application was made using .net core 6.0 which is powerful to build microservices and the api is already prepared for use in container.

- The microservices architecture can be considered, for example, using Docker to manage the packaging of the microservice in a container and Kubernetes to have load balancing and scalability

- Add more records.
