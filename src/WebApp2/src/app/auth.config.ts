import {AuthConfig} from 'angular-oauth2-oidc';

export const authConfig: AuthConfig = {
  issuer: 'http://workflow-manager-identity-service.westeurope.cloudapp.azure.com',
  requireHttps: false,
  redirectUri: `${window.location.origin}`,
  silentRefreshRedirectUri: `${window.location.origin}/silent-refresh.html`,
  clientId: 'spa',
  scope: 'openid profile Processes_Service',
  responseType: 'code'
};
