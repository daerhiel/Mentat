import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

import { Lexeme } from "./models/lexeme";

export class LexiconController {
  constructor(private _http: HttpClient, private _url: string, private _path: string) {
  }

  getLexeme(word: string): Observable<Lexeme> {
    return this._http.get<Lexeme>(`${this._url}/${this._path}/lexeme?word=${word}`);
  }

  getSyntax(text: string): Observable<Lexeme[]> {
    return this._http.post<Lexeme[]>(`${this._url}/${this._path}/syntax`, JSON.stringify(text), {
      headers: { 'Content-Type': 'application/json' }
    });
  }
}
