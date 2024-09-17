# Future consideration
  1. [x] ~~Versionning~~
     - ~~prefered strategy: URL Routing~~
     - ~~Others: HTTP header, Query parameter~~
     - ~~Divide partial class files per version~~
     - ~~header strategy doesn't define standard header format~~
     - could be interesting to use header strategy (custom header or from media : Accept header)  since to levreage default version of api if not mentioned
  a. uncategorized    
     - content negociation ?
     - POST: api/ApplicantEducation/123/Applicant redirect to the appropriate api?

 2. Exception logging
     - [TryHandleAsync()](https://media.licdn.com/dms/image/v2/D4E22AQGv9oGMWMjQcQ/feedshare-shrink_1280/feedshare-shrink_1280/0/1725345017941?e=1728518400&v=beta&t=IfNCQEKallDlWI_Rl-pDFRvg7p8-WNHDIM9WqlPNhqM): <code>: IExceptionHandler TryHandleAsync() -- .AddExceptionHandler<youclass>()</code>
 3. FULLY resfull API (HATEOS level 3)
     - CreatedAtAction()
     - LinkGenerator (DI)
     - Class links { Self, Update, Delete, Http Method}
 4. Sorting: Sortyby= & sortOrder=desc OrderBy()
     - generic properly done use extension method of IQueryableExensions
         - :rotating_light: **Use dynamic lambda generation**
         - `Expression.Lambda()` `Expression.Quote()` `Expression.Call()` `Expression.MakeMemberAccess()` `Expression.Parmeter()` `Expression.Lambda` :rotating_light:
 5. filtering (Create a folder class/controller)
     - Create classes, price?, categories ?
     - Depends what UI intened to offer as filtering
 6. Pagination (Create a folder in common)
     - Query parameter ?items=10&Page=2
     - .Skip(items) * (page-1) .Take(items)
     - Setup a maxx limit and a default value
     - Queryparam class with properties
7. Security
    - CORS Cross Origin Ressoruce Sharing: is a javascript security enforced by browsers (clients) to disallow communication js with naother entity different port...SEARCH
    - Enforce https
     - 401 unauthorized (authentication failed) 403 forbidden: (Authorization failed)  
     a. Authorization
	 b. Authentication
         - JWT + OpenAPI security scheme to test with swagger
         - symmetric encryption (RS256) here
is preferred over symmetric (HS256), since it will use Private Key for Signing and Public key for validation unlike the Symmetric option which will use the same key at both ends (not secure for publicly exposed systems
         - https://www.youtube.com/watch?v=6DWJIyipxzw
         - OAuth/OpenID
         - localhost callback to auth desktop app liek docker
	 c. Rate limit
         - N global request / sec
         - N Api endpoint / secs
         - N API requests in progress at the same time
    d. mécanismes d’autorisation et d’authentification de sécurité mis en place grâce à OAuth, à Active Directory, aux certificats d’application
 8. Caching
    - Server side client side ? ask chatgpt
        - Client side preferable: not changing frequently, static
        - Server side: common data shared between clients
    - Cacheable ressources in the header 
    - How much the resource stays valid, before client request refresh
 9. Searching ?
 10. use Conflict() - custom exceptions ?
 11.  delete shouldn' use body //Todo rethink this, the spec doesn't forbid body entity in a delete verb but some clients
    //could be difficult to implement, could be changed to post
    /*
     * In practice, many HTTP servers and intermediaries may ignore any body present in a DELETE request. As a best practice, most developers avoid sending a body with a DELETE request to ensure compatibility across different servers and clients. If additional information is needed for a deletion operation, it is often recommended to use query parameters or headers instead​(

     * */
   
