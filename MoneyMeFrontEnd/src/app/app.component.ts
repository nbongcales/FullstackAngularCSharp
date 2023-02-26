import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'MoneyMeFrontEnd';
  constructor(private http: HttpClient, private router: Router){
    // this.router.navigate([`${'quoteCalculator'}`]);
    // this.router.navigate(['/apply-now'], { queryParams: { QuoteId: '1002', CustomerId: '3' } });
    this.router.navigate(['/apply-now'], { queryParams: { QuoteId: '2', CustomerId: '2' } });
  }
}
