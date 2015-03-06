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

Responds with 201 created if the static redirection token has never had a redirection token.
If static redirection token already had a redirection location responds with 200 Ok and the body will have the old location.


