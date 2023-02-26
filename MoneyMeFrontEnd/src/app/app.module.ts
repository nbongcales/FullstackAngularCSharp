import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';

import { PostService } from 'src/services/post.service';
import { QuoteCalculatorComponent } from './quote-calculator/quote-calculator.component';
import { ApplyNowComponent } from './apply-now/apply-now.component';

@NgModule({
  declarations: [
    AppComponent,
    QuoteCalculatorComponent,
    ApplyNowComponent
  ],
  imports: [
    BrowserModule,
    NgbModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [PostService],
  bootstrap: [AppComponent]
})
export class AppModule { }
