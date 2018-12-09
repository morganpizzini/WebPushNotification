import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SwPush } from '@angular/service-worker';
import { take } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { Router } from '@angular/router';
// declare var clients: any;
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'myNotificationApp';
  promptSubscription = true;

  constructor(private swPush: SwPush,
    private httpClient: HttpClient,
    private router: Router) {
  }
  ngOnInit() {
    // visualizza o nasconde il subscription button
    this.swPush.subscription.pipe(
      take(1)
    ).subscribe(pushSubscription => {
      if (!pushSubscription) {
        this.promptSubscription = true;
      }
    });
  }

  /**
* Subscribe to web push notification
*/
  subscribeToNotifications() {

    // get current application subscription object
    this.swPush.requestSubscription({
      serverPublicKey: environment.VAPID_PUBLIC_KEY
    })
      .then(sub => {

        this.httpClient.post(environment.ApiUrl + 'Subscription/SubscribeClient', sub)
          .subscribe(data => {
            this.promptSubscription = false;
          }, (err) => {
            // unsubscribe cause of errors
            sub.unsubscribe();
            this.promptSubscription = true;
          });
      })
      .catch(err => console.log(err));
  }
}
