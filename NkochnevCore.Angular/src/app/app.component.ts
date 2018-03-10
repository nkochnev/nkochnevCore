import { Component } from '@angular/core';
import { RoutingState } from './RoutingState';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(routingState: RoutingState) {
    routingState.loadRouting();
  }

  title = 'app';
}
