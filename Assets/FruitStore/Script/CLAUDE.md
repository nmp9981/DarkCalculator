# 프로젝트 가이드라인 (Samsung Engineering 2024_2)

## 기술 스택 및 표준
- **Engine**: Unity 6 기반으로 모든 코드를 작성할 것
- **Language**: C# 최신 버전 (C# 9.0+ 및 .NET 8.0) 패턴을 활용할 것
- **Language Preference**: 모든 설명과 코드 주석은 한국어로 작성할 것

## 코딩 스타일 및 규칙
- **Naming**: 변수와 메서드명은 CamelCase를 사용하며, private 필드는 `_` 접두사를 붙일 것 (예: `_groundMesh`)
- **Optimization**: `Shader.Find`나 `GameObject.Find`는 초기화 시(`Awake`/`Start`)에만 사용하고 반드시 변수에 캐싱하여 사용할 것
- **Unity 6 특화**: 성능 최적화를 위해 가능하면 GPU Instancing 및 신규 Render Graph API 활용을 고려할 것

## Claude 동작 훅 (Hooks)
- 코드를 수정한 후에는 반드시 터미널에서 관련 파일의 변경 사항을 요약해서 보고할 것
- 새로운 기능을 구현할 때는 기존의 `SceneObjectController.cs`와 같은 컨트롤러 구조와 일관성을 유지할 것