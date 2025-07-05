import { WordForm } from "./word-form";

export interface Lexeme {
  language: string;
  lemma: string;
  category: string;
  properties: string[];
  forms: WordForm[];
}
