import {AuthConfig} from 'angular-oauth2-oidc';

// noinspection SpellCheckingInspection
const authConfig: AuthConfig = {
  issuer: 'http://localhost:5000',
  requireHttps: false,
  redirectUri: `${window.location.origin}`,
  silentRefreshRedirectUri: `${window.location.origin}/silent-refresh.html`,
  clientId: 'spa',
  scope: 'openid profile Processes_Service',
  responseType: 'code',
  useSilentRefresh: false
};


export const environment = {
  production: false,
  services: {
    processes: 'http://localhost:8000/api/processes/processes'
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