import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LexiconController } from './lexicon-controller';

@Injectable({
  providedIn: 'root'
})
export class ExplorerService {
  readonly #http = inject(HttpClient);
  readonly #url = 'https://localhost:5001/api';

  readonly lexicon = new LexiconController(this.#http, this.#url, 'lexicon');
}
