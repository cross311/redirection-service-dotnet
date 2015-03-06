Redirection Service
===================

A service to seperate static urls that need to be redirected from where they are redirected.

The original reason for creating this service is to allow technical
communications to update where help files are without having to deploy
new code to the website requesting the help files.

API
---

#### GET: \[static_redirection_token]

Responds with 301 redirection to the provided redirection location.
If no redirection location has been provided a 404 not found will be returned.

#### POST: \[static_redirection_token]

BODY: {'redirection_location':'[redirection_location_url]'}

Responds with 200 ok with the token and location.


