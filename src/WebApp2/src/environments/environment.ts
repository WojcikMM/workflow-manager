// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

// noinspection SpellCheckingInspection
export const environment = {
  production: false,
  firebase: {
    apiKey: 'AIzaSyCdRrYOzDqMNieRXk6DHfOy2G1QC1QFvVg',
    authDomain: 'workflow-manager-dev.firebaseapp.com',
    databaseURL: 'https://workflow-manager-dev.firebaseio.com',
    projectId: 'workflow-manager-dev',
    storageBucket: 'workflow-manager-dev.appspot.com',
    messagingSenderId: '83482061850',
    appId: '1:83482061850:web:690b13a6677f43d60df5a3',
    measurementId: 'G-HTPMGJED4E'
  },
  services: {
    processes: 'https://workflow-manager-processes-service.azurewebsites.net/api/Processes'
  },
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

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
