import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {ProcessesClientService} from './processes';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule,
    // OAuthModule.forRoot(
    //   {resourceServer: {
    //       sendAccessToken: true,
    //       allowedUrls: [
    //         'https://workflow-manager',
    //         'http://localhost'
    //       ]
    //     }}
    // ),
  ],
  exports: [
    HttpClientModule
  ],
  providers: [
    ProcessesClientService
  ]

})
export class ClientsModule {
}
