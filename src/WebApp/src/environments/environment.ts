import {AuthConfig} from 'angular-oauth2-oidc';

// noinspection SpellCheckingInspection
const authConfig: AuthConfig = {
  issuer: 'https://workflow-manager-identity-service-api-dev.azurewebsites.net',
  requireHttps: false,
  redirectUri: `${window.location.origin}`,
  silentRefreshRedirectUri: `${window.location.origin}/silent-refresh.html`,
  clientId: 'spa',
  scope: 'openid profile Processes_Service',
  responseType: 'code'
};

export const environment = {
  production: false,
  services: {
    processes: 'https://workflow-manager-processes-service.azurewebsites.net/api/Processes'
  },
  authentication: authConfig,
  sidebarMenu: [
    {
      label: 'Case Handling',
      items: [
        {
          label: 'Registration',
          path: '/applications/registration'
        },
        {
          label: 'Search',
          path: '/applications/search'
        }
      ]
    },
    {
      label: 'Reporting',
      items: [
        {
          label: 'Standard Reports',
          path: ''
        },
        {
          label: 'Custom Fields',
          path: ''
        },
        {
          label: 'Analytical Data Extract',
          path: ''
        }
      ]
    },
    {
      label: 'Management',
      items: [
        {
          label: 'Processes',
          path: '/management/processes'
        },
        {
          label: 'Statuses',
          path: '/management/statuses'
        },
        {
          label: 'Transactions',
          path: '/management/transactions'
        },
        {
          label: 'Dynamic Fields',
          path: ''
        }
      ]
    },
    {
      label: 'Identity Management',
      items: [
        {
          label: 'Users',
          path: ''
        },
        {
          label: 'Roles',
          path: ''
        }
      ]
    }
  ]
};
