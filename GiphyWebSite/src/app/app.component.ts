import { Component, EventEmitter, Output } from '@angular/core';
import { Subject } from 'rxjs';
import { HttpService } from './services/http.service';
import { GiphyItem } from './_interfaces/giphy.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  private _searchTermSubject = new Subject<string>();
  @Output() setValue: EventEmitter<string> = new EventEmitter();

  title = 'GiphyWebSite';
  public giphyItem: GiphyItem

  constructor(private httpService: HttpService) {
    this._setSearchSubscription();
   }

   private _setSearchSubscription() {
    this._searchTermSubject.pipe(
    ).subscribe((searchValue: string) => {
      this.setValue.emit( searchValue );
    });
  }

  public searchGif(searchTerm: string) {
    this._searchTermSubject.next(searchTerm);
  }

  public getGif = (searchTerm: string) => {
    let route: string = 'https://localhost:44352/api/Giphy/'+ searchTerm;
    this.httpService.getData(route)
      .subscribe((result) => {
        this.giphyItem = result as GiphyItem;
      },
        (error) => {
          console.error(error);
        });
  }
  ngOnDestroy() {
    this._searchTermSubject.unsubscribe();
  }
}
