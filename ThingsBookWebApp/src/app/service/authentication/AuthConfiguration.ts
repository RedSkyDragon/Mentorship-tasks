import { AuthConfig } from 'angular-oauth2-oidc';

export const AuthConfiguration: AuthConfig = {
    issuer: 'http://localhost/identityserver',
    redirectUri: 'http://localhost:4200',
    clientId: 'AngularClient',
    scope: 'openid profile things-book',
    postLogoutRedirectUri: 'http://localhost:4200',
    silentRefreshRedirectUri: 'http://localhost:4200/silent-refresh.html'
};

