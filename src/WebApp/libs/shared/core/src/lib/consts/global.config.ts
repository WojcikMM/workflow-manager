export const GLOBAL_CONFIG = {
  SERVICES: {
    CONFIGURATION_SERVICE: 'http://localhost:8000/api/configuration',
    OPERATIONS_SERVICE: ''
  },
  AUTH_CONFIG: {
    authority: 'http://localhost:5000',
    client_id: 'spa',
    redirect_uri: `${window.location.origin}/callback.html`,
    post_logout_redirect_uri: `${window.location.origin}/signout-callback.html`,
    response_type: 'code',
    scope: 'openid profile Configuration_Service',
    silent_redirect_uri: `${window.location.origin}/renew-callback.html`,
    automaticSilentRenew: false,
    monitorSession: false,
    loadUserInfo: true
  },
  SIDEBAR_MENU_CONFIG: [
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
